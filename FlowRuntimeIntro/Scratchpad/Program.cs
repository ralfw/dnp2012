using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using npantarhei.runtime;
using npantarhei.runtime.contract;
using npantarhei.runtime.messagetypes;
using npantarhei.runtime.patterns;

namespace Scratchpad
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new FlowRuntimeConfiguration();

            // aus embeddded resource laden
            config.AddStreamsFrom("Scratchpad.flows.flow", Assembly.GetExecutingAssembly());

        //            // im 3GL Quelltext
        //            config.AddStreamsFrom(@"/
        //                                    .in, Transform.in
        //                                    Transform.out, .out
        //
        //                                    Transform
        //                                    .in, toUpper
        //                                    toUpper, reverse
        //                                    reverse, .out");

        //            // als Liste von Streams
        //            config.AddStream(".in", "Transform.in")
        //                    .AddStream("Transform.out", ".out")

        //                    .AddStream("Transform/.in", "Transform/toUpper")
        //                    .AddStream("Transform/toUpper", "Transform/reverse")
        //                    .AddStream("Transform/reverse", "Transform/.out");

            config.AddFunc<string, string>("toUpper", Program.ToUpper);
            //config.AddOperation(new ToUpperOperation());
            config.AddFunc<string, string>("reverse", Program.Reverse);

            config.AddOperation(new Get_text());
            config.AddAction<string>("display_text", (string text) => Console.WriteLine("-> {0}\n", text), true);

            using(var fr = new FlowRuntime(config))
            {
                //fr.Message += Console.WriteLine;
                //fr.Throttle(1000);

                //fr.Process(".in", string.Join(" ", args));
                //fr.WaitForResult(_ => Console.WriteLine(_.Data));
                fr.Process(".run");
                fr.WaitForResult();
            }
        }


        static string Transform(string text)
        {
            var x = ToUpper(text);
            return Reverse(x);
        }

        static string ToUpper(string text)
        {
            return text.ToUpper();
        }

        static string Reverse(string text)
        {
            return new string(text.ToCharArray().Reverse().ToArray());
        }
    }


    class Get_text : AOperation
    {
        public Get_text() : base("get_text") {}

        protected override void Process(IMessage input, Action<IMessage> continueWith, Action<FlowRuntimeException> unhandledException)
        {
            Console.Write("Text: ");
            var text = Console.ReadLine();
            if (text == "")
                continueWith(new Message(base.Name + ".exit", null));
            else
                continueWith(new Message(base.Name + ".text", text));
        }
    }

    class ToUpperOperation : AOperation
    {
        public ToUpperOperation() : base("ToUpper") {}

        protected override void Process(IMessage input, 
                                        Action<IMessage> continueWith, 
                                        Action<FlowRuntimeException> unhandledException)
        {
            var text = input.Data.ToString();
            continueWith(new Message(base.Name, text.ToUpper()));
        }
    }


    class Transform
    {
        public Transform()
        {
            var toUpper = new ToUpper();
            var reverse = new Reverse();

            _process += toUpper.Process;
            toUpper.Result += reverse.Process;
            reverse.Result += _ => Result(_);
        }

        private readonly Action<string> _process;
        public void Process(string text) { _process(text); }

        public event Action<string> Result;
    }

    class ToUpper
    {
        public void Process(string text)
        {
            Result(text.ToUpper());
        }

        public event Action<string> Result;
    }

    class Reverse
    {
        public void Process(string text)
        {
            Result(new string(text.ToCharArray().Reverse().ToArray()));
        }

        public event Action<string> Result;
    }
}
