using System;
using System.Collections.Generic;
using System.Text;

namespace CodeBuilder
{
    public class Field
    {
        public string FieldName;
        public string Keyword;
        public List<Field> Fields = new List<Field>();
        private const int IndentSize = 2;

        public Field()
        {
            
        }

        public Field(string fieldName, string keyword)
        {
            FieldName = fieldName;
            Keyword = keyword;
        }

        private string ToStringImpl(int indent)
        {
            var sb = new StringBuilder();
            var i = new string(' ', IndentSize * indent);
            sb.AppendLine($"{i}public {Keyword} {FieldName}");
            sb.AppendLine("{");
            if (Fields != null)
            {
                foreach (var field in Fields)
                {
                    sb.Append(new string(' ', IndentSize * (indent + 1)));
                    sb.AppendLine($"{i}public {field.Keyword} {field.FieldName};");
                }
            }
            sb.AppendLine("}");

            return sb.ToString();
        }

        public override string ToString()
        {
            return ToStringImpl(0);
        }
    }

    public class CodeBuilder
    {
        private readonly string className;
        Field rootClass = new Field();

        private CodeBuilder()
        {

        }

        public CodeBuilder(string className)
        {
            if (className is null)
            {
                throw new ArgumentNullException(nameof(className));
            }

            rootClass.FieldName = className;
            rootClass.Keyword = "class";
        }

        public CodeBuilder AddField(string fieldName, string keyword)
        {
            var field = new Field(fieldName, keyword);
            rootClass.Fields.Add(field);
            return this;
        }

        public override string ToString()
        {
            return rootClass.ToString();
        }

        public void clear()
        {
            rootClass = new Field{FieldName = className};
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var cb = new CodeBuilder("Person").AddField("Name", "string").AddField("Age", "int");
            Console.WriteLine(cb);

            Console.ReadKey();
        }
    }
}
