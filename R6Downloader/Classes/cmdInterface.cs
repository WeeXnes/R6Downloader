using System.Diagnostics;

namespace cmdInterface
{
    public class CMD
    {
        public static void execute(string[] input)
        {
            string arguments = "/C";
            foreach (string str in input)
            {
                arguments = arguments + " && " + str;
            }
            Process.Start("CMD.exe", arguments);
        }
    }
}