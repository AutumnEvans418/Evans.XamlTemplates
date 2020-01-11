using System.Collections.Generic;

namespace Evans.XamlTemplates
{
    public class Iterator<T>
    {
        public int Index { get; set; }
        public IList<T> Input { get; set; } = new List<T>();

        public T Peek(int offset = 0)
        {
            if (Input.Count > Index + offset)
                return Input[Index + offset];
            return default;
        }

        public void Move()
        {
            Index++;
        }
    }
}