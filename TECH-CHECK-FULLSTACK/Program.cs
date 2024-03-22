using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECH_CHECK_FULLSTACK.Models;

namespace TECH_CHECK_FULLSTACK
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Comment> comments = new List<Comment>();
            string path = "C:\\Users\\khetha\\source\\repos\\TECH-CHECK-FULLSTACK\\TECH-CHECK-FULLSTACK\\Docs";
            string[] files = Directory.GetFiles(path);

            foreach (var file in files)
            {
                var coms = File.ReadAllLines(file);
                CommentAnalyzer commentAnalyzer = new CommentAnalyzer(coms);

                Parallel.Invoke(
                    () => comments.Add(commentAnalyzer.Analyze("SHORTER_THAN_15")),
                    () => comments.Add(commentAnalyzer.Analyze("MOVER_MENTIONS")),
                    () => comments.Add(commentAnalyzer.Analyze("SHAKER_MENTIONS")),
                    () => comments.Add(commentAnalyzer.Analyze("QUESTIONS")),
                    () => comments.Add(commentAnalyzer.Analyze("SPAM")));
            }

            List<Comment> results = comments.GroupBy(x => x.CommentType)
                .Select(x => new Comment()
                {
                    CommentType = x.Key,
                    TotalComments = x.Sum(t => t.TotalComments)
                })
                .ToList();

            Console.WriteLine("RESULTS\n=======");
            foreach (var comment in results)
            {
                Console.WriteLine(comment.CommentType + " : " + comment.TotalComments);
            }

            Console.Read();
        }
    }
}
