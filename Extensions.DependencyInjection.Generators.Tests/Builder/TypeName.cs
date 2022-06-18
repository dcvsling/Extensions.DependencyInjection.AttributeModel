using System;
using System.Collections.Generic;
using System.Linq;

using static System.Net.Mime.MediaTypeNames;

namespace Extensions.DependencyInjection.Generators.Tests.Builder;

public record TypeName
{
    public TypeName(): this(string.Empty) { }
    public TypeName(string name)
    {
        Name = name;
    }
    public string Name { get; init; } = string.Empty;
    public ICollection<TypeName> TypeParameters { get; init; } = new List<TypeName>();
    public override string ToString()
        => $"{Name}{ (TypeParameters.Any() 
            ? $"<{string.Join(",", TypeParameters)}>" 
            : string.Empty) }";
    public static implicit operator TypeName(string name)
    {
        var index = name.IndexOf('<');
        if (index < 0)
            return new TypeName(name);
        var visitor = new TypeNameVisitor();
        
        visitor.Visit(name[(index + 1)..^1]);
        return new TypeName { Name = name[..index], TypeParameters = visitor.Names };
    }
    public static implicit operator string(TypeName name)
        => name.ToString();


    internal class TypeNameVisitor
    {
        internal TypeNameVisitor()
        {

        }
        internal TypeNameVisitor(TypeNameVisitor parent)
        {
            _parent = parent;
        }
        private static readonly char[] _flag = new[] { '<', '>', ',' };
        private readonly TypeNameVisitor? _parent;

        public List<TypeName> Names { get; } = new List<TypeName>();

        public void Visit(string text)
        {
            var index = text.IndexOfAny(_flag);
            if (index < 0)
            {
                Names.Add(new TypeName(text));
                return;
            }
            (text[index] switch
            {
                '<' => VisitLeftBrace,
                ',' => VisitSplitor,
                '>' => VisitRightBrace,
                _ => (Action<string, string>)((_, _) => throw new NotSupportedException())
            }).Invoke(text[..index].Trim(), text[(index + 1)..].Trim());
        }
        public void VisitLeftBrace(string left, string right)
        {
            var visitor = new TypeNameVisitor(this);
            var name = new TypeName(left);
            Names.Add(name);
            visitor.Visit(right);
            visitor.Names.ForEach(name.TypeParameters.Add);
        }
        public void VisitRightBrace(string left, string right)
        {
            if (!string.IsNullOrWhiteSpace(left))
                Names.Add(new TypeName(left));
            (_parent ?? this).Visit(right.Length > 0 && right.StartsWith(",") ? right[1..] : right);
        }
        public void VisitSplitor(string left, string right)
        {
            Names.Add(new TypeName(left));
            Visit(right);
        }
    }

}
