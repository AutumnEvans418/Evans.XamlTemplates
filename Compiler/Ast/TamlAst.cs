using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Evans.XamlTemplates
{
    public class TamlAst : Iterator<Token>
    {


        protected override Token Default => new Token(TokenType.EndOfFile, Index, 0);

        private void Eat(TokenType token)
        {
            var p = Peek();
            if (p is { } t && t.TokenType != token)
            {
                throw new CompileException($"Expected {token} but was {t.TokenType}", p);
            }
            Move();
        }

        Template GetTemplate()
        {
            var template = new Template(Peek());
            Eat(TokenType.At);
            if (Current.Value == null)
            {
                throw new CompileException("Expected classname but was null", Current);
            }
            template.ClassName = Current.Value;
            Eat(TokenType.Id);
            Eat(TokenType.ParenthesesOpen);
            while (Peek() is { } token
                   && token.TokenType != TokenType.ParenthesesClose
                   && token.TokenType != TokenType.EndOfFile)
            {
                template.Parameters.Add(GetParameter());
                if (Peek() is { } t && t.TokenType != TokenType.ParenthesesClose)
                {
                    Eat(TokenType.Comma);
                }
            }
            Eat(TokenType.ParenthesesClose);

            template.Body = GetBody();
            return template;
        }

        private string GetTypeName()
        {
            var name = Peek().Value;

            if (name == null) throw new CompileException("Expected TypeName but was null", Current);

            Eat(TokenType.Id);
            if (Peek().TokenType == TokenType.BracketOpen)
            {
                name += Peek().Value;
                Eat(TokenType.BracketOpen);
                while (Peek() is { } token
                       && token.TokenType != TokenType.BracketClose
                       && token.TokenType != TokenType.EndOfFile)
                {
                    name += Peek().Value;
                    Eat(TokenType.Id);
                }
                name += Peek().Value;
                Eat(TokenType.BracketClose);
            }


            return name;
        }

        private Parameter GetParameter()
        {
            var parameter = new Parameter(Peek());

            parameter.Type = GetTypeName();
            var name = Peek().Value;
            parameter.Name = name ?? throw new CompileException("Expected parameter name but was null", Current);
            Eat(TokenType.Id);

            if (Peek().TokenType == TokenType.Equal)
            {
                Eat(TokenType.Equal);

                if (Peek().TokenType == TokenType.Id)
                {
                    parameter.DefaultValue = Peek().Value;
                    Eat(TokenType.Id);
                }
                else
                {
                    parameter.DefaultValue = Peek().Value;
                    Eat(TokenType.Quote);
                }
            }

            return parameter;
        }


        private Body GetBody()
        {
            var xml = "";
            var beginning = Peek();
            Eat(TokenType.CurlyBracketOpen);
            while (Peek() is { } token
                   && token.TokenType != TokenType.CurlyBracketClose
                   && token.TokenType != TokenType.EndOfFile)
            {

                if (token.TokenType == TokenType.Id && Peek(1) is { } t && t.TokenType != TokenType.Equal && t.TokenType != TokenType.Colon)
                {
                    xml += token.Value + " ";
                }
                else if (token.TokenType == TokenType.Quote)
                {
                    xml += $"\"{token.Value}\" ";
                }
                else
                {
                    xml += token.Value;
                }
                Move();
            }
            Eat(TokenType.CurlyBracketClose);




            try
            {
                //check that it's valid xml
                //var result = XDocument.Parse(xml);
                

                var declarations = _defaultXmlDeclaration.Aggregate(" ", (s, s1) => s + " " + s1);

                //var index = xml.IndexOf('>');

                //if (index < 0)
                //{
                //    throw new CompileException("Could not find '>' in xml", Peek());
                //}
                //var addedInsert = xml.Insert(index-1, declarations);

                //var result = XDocument.Parse(addedInsert);

                var reader = XDocument.Parse($"<_Root {declarations}>" + xml + "</_Root>");

                var body = new Body(beginning, reader);
                body.Xml = reader;
                body.Controls = ParseXml(reader);
                return body;
            }
            catch (XmlException e)
            {
                throw new CompileException($"Failed to parse xml: {e}", Peek());
            }
        }



        private List<Control> ParseXml(XDocument reader)
        {
            if(reader.Root == null) throw new ArgumentNullException(nameof(reader.Root));
            return RecurseXml(reader.Root.Descendants().ToList());
        }

        List<Control> RecurseXml(List<XElement> parentNode)
        {
            var controls = new List<Control>();
            foreach (XElement node in parentNode)
            {
                if (node == null) continue;
                var control = new Control(Peek(), node);


                //control.Namespace = node.GetDefaultNamespace().NamespaceName;

                //control.Name = node.Name.ToString();
                foreach (XAttribute attribute in node.Attributes())
                {
                    var property = new ControlProperty(Peek());
                    property.Name = attribute.Name.LocalName;
                    property.Value = attribute.Value;
                    control.ControlProperties.Add(property);
                }
                controls.Add(control);
                //control.ChildControls = RecurseXml(node.Descendants().ToList());
            }
            return controls;
        }

        Program GetProgram()
        {
            var program = new Program(Peek());

            if (Peek().TokenType != TokenType.At)
            {
                Eat(TokenType.EndOfFile);
            }
            while (Current.TokenType != TokenType.EndOfFile)
            {
                program.Templates.Add(GetTemplate());
            }
            return program;
        }

        private IEnumerable<string> _defaultXmlDeclaration = new List<string>();
        public Program Evaluate(IEnumerable<Token> tokens, IEnumerable<string>? defaultXmlDeclarations = null)
        {
            _defaultXmlDeclaration = defaultXmlDeclarations ?? new List<string>();
            Index = 0;
            Input = tokens.ToList();
            return GetProgram();
        }
    }
}