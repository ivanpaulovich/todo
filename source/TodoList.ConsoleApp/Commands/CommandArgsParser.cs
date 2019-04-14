namespace TodoList.ConsoleApp.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    //
    // Code from https://github.com/nelyom/cmdLineTokenizer/blob/master/cmdLineTokenizer/Tokenizer.cs
    //

    public static class CommandArgsParser
    {
        public static List<string> TokenizeCommandLineToList(string commandLine)
        {

            List<string> tokens = new List<string>();
            StringBuilder token = new StringBuilder(255);
            var sections = commandLine.Split(' ');

            for (int curPart = 0; curPart < sections.Length; curPart++)
            {

                // We are in a quoted section!!
                if (sections[curPart].StartsWith("\""))
                {
                    //remove leading "
                    token.Append(sections[curPart].Substring(1));
                    int quoteCount = 0;

                    //Step backwards from the end of the current section to find the count of quotes from the end.
                    //This will exclude looking at the first character, which was the " that got us here in the first place.
                    for (; quoteCount < sections[curPart].Length - 1; quoteCount++)
                    {
                        if (sections[curPart][sections[curPart].Length - 1 - quoteCount] != '"')
                        {
                            break;
                        }
                    }

                    // if we didn't have a leftover " (i.e. the 2N+1), then we should continue adding in the next section to the current token.
                    while (quoteCount % 2 == 0 && (curPart != sections.Length - 1))
                    {
                        quoteCount = 0;
                        curPart++;

                        //Step backwards from the end of the current token to find the count of quotes from the end.
                        for (; quoteCount < sections[curPart].Length; quoteCount++)
                        {
                            if (sections[curPart][sections[curPart].Length - 1 - quoteCount] != '"')
                            {
                                break;
                            }
                        }

                        token.Append(' ').Append(sections[curPart]);
                    }

                    //remove trailing " if we had a leftover
                    //if we didn't have a leftover then we go to the end of the command line without an enclosing " 
                    //so it gets treated as a quoted argument anyway
                    if (quoteCount % 2 != 0)
                    {
                        token.Remove(token.Length - 1, 1);
                    }
                    token.Replace("\"\"", "\"");
                }
                else
                {
                    //Not a quoted section so this is just a boring parameter
                    token.Append(sections[curPart]);
                }

                //strip whitespace (because).
                if (!string.IsNullOrEmpty(token.ToString().Trim()))
                    tokens.Add(token.ToString().Trim());

                token.Clear();
            }

            //return the array in the same format args[] usually turn up to main in.
            return tokens;
        }

        public static string[] TokenizeCommandLineToStringArray(string commandLine)
        {
            List<string> tokens = TokenizeCommandLineToList(commandLine);
            return tokens.ToArray<string>();
        }

        public static bool Match(string[] args, string[] tokens)
        {
            bool match = false;

            foreach (var token in tokens)
                if (string.Compare(args[0].Trim(), token, StringComparison.CurrentCultureIgnoreCase) == 0)
                    match = true;

            return match;
        }
    }
}