namespace Common
{
    using System;

    public class WindowRepresentation
    {
        public IntPtr Pointer { get; set; }
        public string Title { get; set; }
        public string ClassName { get; set; }

        public override string ToString()
        {
            return string.Format("{0} - {1}", Title, ClassName);
        }
    }
}