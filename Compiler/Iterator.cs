using System.Collections.Generic;

namespace Evans.XamlTemplates
{
    public class Iterator<T> where T : new()
    {
        protected int Index { get; set; }
        protected IList<T> Input { get; set; } = new List<T>();
        protected T Current => Peek();

        protected virtual T Default => new T();

        protected T Peek(int offset = 0)
        {
            if (Input.Count > Index + offset)
                return Input[Index + offset];
            return Default;
        }

        protected virtual void Move()
        {
            Index++;
        }
    }
}