using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


namespace TECH_CHECK_FULLSTACK.Models
{
    public class CommentAnalyzer
    {
        private string[] file;

        public CommentAnalyzer(string[] file)
        {
            this.file = file;
        }

        public  Comment Analyze(string commentType)
        {
            Comment comment = new Comment();
            string pattern = "(http|https|ftp):\\/\\/(\\S*)";
            Regex rg = new Regex(pattern);
            List<string> result = new List<string>(file);

            try
            {
                switch (commentType)
                {
                    case "SHORTER_THAN_15":
                        comment.CommentType = "SHORTER_THAN_15";
                        comment.TotalComments = result.Count(x => x.Length < 15);
                        break;
                    case "MOVER_MENTIONS":
                        comment.CommentType = "MOVER_MENTIONS";
                        comment.TotalComments = result.Count(x => x.Contains("Mover"));
                        break;
                    case "SHAKER_MENTIONS":
                        comment.CommentType = "SHAKER_MENTIONS";
                        comment.TotalComments = result.Count(x => x.Contains("Shaker"));
                        break;
                    case "QUESTIONS":
                        comment.CommentType = "QUESTIONS";
                        comment.TotalComments = result.Count(x => x.Contains("?"));
                        break;
                    case "SPAM":
                        comment.CommentType = "SPAM";
                        comment.TotalComments = result.Count(x => rg.Matches(x).Count > 0);
                        break;
                    default:
                        break;
                }              
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
                Console.WriteLine(e.StackTrace);
            }

            return comment;
        }
    }
}
