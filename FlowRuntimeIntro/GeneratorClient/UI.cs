using System;
using GeneratorContract;
using npantarhei.runtime.contract;

namespace GeneratorClient
{
    [EventBasedComponent]
    public class UI
    {
        public void Ask()
        {
            while (true)
            {
                Console.Write("  req (*, a..., x...): ");
                var req = Console.ReadLine();
                if (req == "")
                {
                    Exit();
                    return;
                }
                else
                {
                    if (req.StartsWith("x"))
                        RequestException(req);
                    else if (req.StartsWith("a"))
                        RequestAll(new Question{Text=req});
                    else
                        Request(req);
                }
            }
        }

        public void Display(string response)
        {
            Console.WriteLine(response);
        }

        public void Error(string description)
        {
            Console.WriteLine("*** Error during processing on server: {0}", description);
        }

        public void DisplayAll(Answer answer)
        {
            foreach (var part in answer.Parts)
                Console.WriteLine(part);
        }

        public event Action<string> Request;
        public event Action<string> RequestException;
        public event Action<Question> RequestAll;
        public event Action Exit;
    }
}