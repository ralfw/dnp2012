using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using npantarhei.runtime;
using npantarhei.runtime.contract;
using npantarhei.runtime.operations;

namespace SyncAsyncConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new FlowRuntimeConfiguration()
                                .AddStreamsFrom(@"
                                                    /
                                                    ui.request, generate
                                                    generate, ui.show_results
                                                 ")
                                .AddEventBasedComponent("ui", new Program.UI())
                                .AddStaticOperations(typeof (Program.Generator));
            using(var fr = new FlowRuntime(config, new Schedule_for_async_breadthfirst_processing()))
            {
                Program.UI.Instance.Ask_for_requests();
            }
        }


        [EventBasedComponent]
        class UI
        {
            public static UI Instance;

            public UI()
            {
                Instance = this;
            }

            public void Ask_for_requests()
            {
                while(true)
                {
                    Console.Write("req: "); var req = Console.ReadLine();
                    if (req == "") return;

                    Request(req);
                }
            }


            public void Show_results(string result)
            {
                Console.WriteLine("<{0}>", result);
            }


            public event Action<string> Request;
        }


        [StaticOperations]
        class Generator
        {
            //[AsyncMethod]
            public static void Generate(string req, Action<string> result)
            {
                foreach(var c in req)
                {
                    result(string.Format("'{0}'", c));
                    Thread.Sleep(500);
                }   
            }
        }
    }
}
