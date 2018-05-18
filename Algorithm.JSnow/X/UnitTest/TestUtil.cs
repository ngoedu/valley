using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snow.XUnitTest
{
    public sealed class TestUtil
    {
        public static string GetFileContent(string srcPath, string SampleFile)
        {
            var content = System.IO.File.ReadAllText(srcPath + "\\" + SampleFile);
            return content;
        }

        public static string ExtractJSContent(string content)
        {
            //load test script form file
            int idx1 = content.IndexOf("<script>");
            int idx2 = content.IndexOf("</script>");
            content = content.Substring(idx1 + 8, idx2 - idx1 - 8);
            return content;
        }
    }
}
