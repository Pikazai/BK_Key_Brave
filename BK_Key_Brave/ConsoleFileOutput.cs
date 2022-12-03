

using System;
using System.IO;
using System.Text;

namespace ConsoleProgram
{
    public class ConsoleFileOutput : TextWriter
    {
        private Encoding encoding = Encoding.UTF8;
        private StreamWriter writer;
        private TextWriter console;

        public override Encoding Encoding => this.encoding;

        public ConsoleFileOutput(string filePath, TextWriter console, Encoding encoding = null)
        {
            if (encoding != null)
                this.encoding = encoding;
            this.console = console;
            this.writer = new StreamWriter(filePath, false, this.encoding);
            this.writer.AutoFlush = true;
        }

        public override void Write(string value)
        {
            Console.SetOut(this.console);
            Console.Write(value);
            Console.SetOut((TextWriter)this);
            this.writer.Write(value);
        }

        public override void WriteLine(string value)
        {
            Console.SetOut(this.console);
            Console.WriteLine(value);
            this.writer.WriteLine(value);
            Console.SetOut((TextWriter)this);
        }

        public override void Flush() => this.writer.Flush();

        public override void Close() => this.writer.Close();

        public new void Dispose()
        {
            this.writer.Flush();
            this.writer.Close();
            this.writer.Dispose();
            base.Dispose();
        }
    }
}
