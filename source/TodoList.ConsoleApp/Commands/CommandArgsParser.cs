namespace TodoList.ConsoleApp.Commands
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    //
    // Code from https://github.com/nelyom/cmdLineTokenizer/blob/master/cmdLineTokenizer/Tokenizer.cs
    //

    public static class CommandArgsParser
    {
        /// <summary>
        /// Pulls apart the input string, traditionally the command line, and tokenises it to each item.  
        /// The GOD separator between each argument is SPACE.
        /// If the input contains double quoted (i.e. " ) arguments, these will be returned without quotes, unless the arument contains quotes (whether escaped or unescaped).
        /// </summary>
        /// <param name="commandLine">The command line used to launch the application.  Usually Environment.CommandLine</param>
        /// <returns>List<string></returns>
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

        /// <summary>
        /// Pulls apart the input string, traditionally the command line, and tokenises it to each item.  
        /// The GOD separator between each argument is SPACE.
        /// If the input contains double quoted (i.e. " ) arguments, these will be returned without quotes, unless the arument contains quotes (whether escaped or unescaped).
        /// </summary>
        /// <param name="commandLine">The command line used to launch the application.  Usually Environment.CommandLine</param>
        /// <returns>string[]</returns>
        public static string[] TokenizeCommandLineToStringArray(string commandLine)
        {
            List<string> tokens = TokenizeCommandLineToList(commandLine);
            return tokens.ToArray<string>();
        }
    }
}