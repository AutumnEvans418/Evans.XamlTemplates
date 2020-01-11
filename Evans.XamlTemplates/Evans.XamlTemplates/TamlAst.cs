using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Evans.XamlTemplates
{
    public class ControlProperty : Node
    {
        public ControlProperty(Token token) : base(token)
        {
        }
        public string Name { get; set; } = "";
        public string Value { get; set; } = "";
        public bool IsParameter => Value.StartsWith("@");
    }

    public class Control : Node
    {
        public Control(Token token) : base(token)
        {
        }

        public string Name { get; set; }

        public List<ControlProperty> ControlProperties { get; set; } = new List<ControlProperty>();

        public bool HasParameter => ControlProperties.Any(p => p.IsParameter);
    }

    public class Body : Node
    {
        public Body(Token token) : base(token)
        {
        }
        public string Xml { get; set; } = "";
        public List<Control> Controls { get; set; } = new List<Control>();
    }

    public class Parameter : Node
    {
        public Parameter(Token token) : base(token)
        {
        }
        public string Type { get; set; } = "";
        public string Name { get; set; } = "";
    }

    public class Template : Node
    {
        public Template(Token token) : base(token)
        {
        }

        public string ClassName { get; set; } = "";
        public List<Parameter> Parameters { get; set; } = new List<Parameter>();
        public Body? Body { get; set; }
    }

    public class Program : Node
    {
        public Program(Token token) : base(token)
        {
        }

        public List<Template> Templates { get; set; }

    }

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

        private Body GetBody()
        {
            var body = new Body(Peek());
            Eat(TokenType.CurlyBracketOpen);
            while (Peek() is { } token && token.TokenType != TokenType.CurlyBracketClose)
            {
                xml += token.Value;
                Move();
            }
            Eat(TokenType.CurlyBracketClose);
            body.Xml = xml;

            //body.Controls = ParseXml(xml);
            return body;
        }

        private List<Control> ParseXml(string xml)
        {
            var controls = new List<Control>();

            var reader = new XmlDocument();

            reader.LoadXml(xml);

            RecurseXml(reader.ChildNodes.Cast<XmlNode>().ToList(), controls);
            return controls;
        }

        void RecurseXml(List<XmlNode> parentNode, List<Control> controls)
        {
            foreach (XmlNode node in parentNode)
            {
                if(node == null) continue;
                var control = new Control(Peek());
                control.Name = node.Name;
                foreach (XmlAttribute attribute in node.Attributes)
                {
                    var property = new ControlProperty(Peek());
                    property.Name = attribute.Name;
                    property.Value = attribute.Value;
                    control.ControlProperties.Add(property);
                }
                controls.Add(control);
                RecurseXml(node.ChildNodes.Cast<XmlNode>().ToList(), controls);
            }
        }

        Program GetProgram()
        {
            var program = new Program(Peek());

            if (Peek().TokenType != TokenType.At)
            {
                Eat(TokenType.EndOfFile);
            }
            while (Peek() != null)
            {
                program.Templates.Add(GetTemplate());
            }
            return program;
        }

        public Node Evaluate(IEnumerable<Token> tokens)
        {
            Input = tokens.ToList();
            return GetProgram();
        }
    }
}