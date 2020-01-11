using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Evans.XamlTemplates
{
    public class TamlAst : Iterator<Token>
    {
        public void Eat(TokenType token)
        {
            var p = Peek();
            if (p == null)
            {
                throw new InvalidOperationException($"Expected {token} but was null");
            }
            if (p is Token t && t.TokenType != token)
            {
                throw new InvalidOperationException($"Expected {token} but was {t.TokenType}");
            }
            Move();
        }

        Template GetTemplate()
        {
            var template = new Template(Peek());
            Eat(TokenType.At);
            template.ClassName = Peek().Value;
            Eat(TokenType.Id);
            Eat(TokenType.ParenthesesOpen);
            while (Peek() is { } token && token.TokenType != TokenType.ParenthesesClose)
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

        private Parameter GetParameter()
        {
            var parameter = new Parameter(Peek());

            parameter.Type = Peek().Value;
            Eat(TokenType.Id);
            parameter.Name = Peek().Value;
            Eat(TokenType.Id);
            return parameter;
        }

        //Control GetControl()
        //{
        //    var control = new Control(Peek());

        //    Eat(TokenType.BracketOpen);
        //    var name = Peek().Value;
        //    Eat(TokenType.Id);

        //    control.Name = name;
        //    while (Peek() is { } token
        //           && token.TokenType != TokenType.BracketClose)
        //    {
        //        if (Current is { } t && t.TokenType == TokenType.ForwardSlash)
        //        {
        //            Eat(TokenType.ForwardSlash);
        //        }
        //        else
        //        {
        //            control.ControlProperties.Add(GetProperty());
        //        }
        //    }
        //    Eat(TokenType.BracketClose);
        //    return control;
        //}

        //private ControlProperty GetProperty()
        //{
        //    var controlProperty = new ControlProperty(Peek());

        //    controlProperty.Name = Peek().Value;
        //    Eat(TokenType.Id);
        //    Eat(TokenType.Equal);
        //    controlProperty.Value = Peek().Value;
        //    Eat(TokenType.Quote);
        //    return controlProperty;
        //}

        private Body GetBody()
        {
            var xml = "";
            var beginning = Peek();
            Eat(TokenType.CurlyBracketOpen);
            while (Peek() is { } token && token.TokenType != TokenType.CurlyBracketClose)
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

            var reader = new XmlDocument();

            reader.LoadXml(xml);
            var body = new Body(beginning, reader);

            body.Xml = reader;

            body.Controls = ParseXml(reader);
            return body;
        }



        private List<Control> ParseXml(XmlDocument reader)
        {
            return RecurseXml(reader.ChildNodes.Cast<XmlNode>().ToList());
        }

        List<Control> RecurseXml(List<XmlNode> parentNode)
        {
            var controls = new List<Control>();
            foreach (XmlNode node in parentNode)
            {
                if (node == null) continue;
                var control = new Control(Peek(), node);
                control.Name = node.Name;
                foreach (XmlAttribute attribute in node.Attributes)
                {
                    var property = new ControlProperty(Peek());
                    property.Name = attribute.Name;
                    property.Value = attribute.Value;
                    control.ControlProperties.Add(property);
                }
                controls.Add(control);
                control.ChildControls = RecurseXml(node.ChildNodes.Cast<XmlNode>().ToList());
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

        public Program Evaluate(IEnumerable<Token> tokens)
        {
            Input = tokens.ToList();
            return GetProgram();
        }
    }
}