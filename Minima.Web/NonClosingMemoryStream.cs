using System.IO;
//+
namespace Minima.Web
{
    public class NonClosingMemoryStream : MemoryStream
    {
        //- @Close -//
        public override void Close()
        {
            this.Position = 0;
        }
    }
}