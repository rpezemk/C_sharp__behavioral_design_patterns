using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chain_Of_Responsibility_Zwei
{
    class Chain_of_responsibility
    {
        static void Main(string[] args)
        {
            List<Problem> problems = new List<Problem>();
            problems.Add(new Problem() { description = "łatwy", complicacy = 2 });
            problems.Add(new Problem() { description = "średni", complicacy = 8 });
            problems.Add(new Problem() { description = "trudny", complicacy = 87 });
            problems.Add(new Problem() { description = "mega trudny", complicacy = 104 });


            Szeregowiec szeregowiec = new Szeregowiec();
            Kapitan kapitan = new Kapitan();
            Major major = new Major();

            szeregowiec.SetNext(kapitan).SetNext(major);

            Client client = new Client();

            foreach (Problem problem in problems)
            {
                client.ManageRequest(szeregowiec, problem);
            }
            Console.ReadLine();          
        }
    }

    public class Client
    {

        public void ManageRequest(IHandler handler, Problem problem)
        {
            var result = handler.Handle(problem);
            if (result != null)
            {
                Console.WriteLine(result.ToString());
            }
            else
            {
                Console.WriteLine($"{problem.description} o trudności {problem.complicacy} jest nieprzerobiony");
            }
        }
    }

    public interface IHandler
    {
        IHandler SetNext(IHandler handler);
        object Handle(Problem problem);
    }

    public class AbstractHandler : IHandler
    {
        public IHandler _nexHandler;
        

        public virtual IHandler SetNext(IHandler request)
        {
            this._nexHandler = request;
            return request;
        }

        public virtual object Handle(Problem problem)
        {
            if (_nexHandler != null)
            {
                return this._nexHandler.Handle(problem); ;
            }
            else
            {
                return null;
            }
        }
    }

    public class Szeregowiec : AbstractHandler
    {
        public override object Handle(Problem problem)
        {
            if (problem.complicacy <= 2)
            {
                return $"Szeregowiec zajął się {problem.description}, {problem.complicacy}";
            }
            else
            {
                return base.Handle(problem);
            }
        }

    }

    public class Kapitan : AbstractHandler
    {
        public override object Handle(Problem problem)
        {
            if (problem.complicacy >= 3 && problem.complicacy <= 10)
            {
                return $"Kapitan zajął się {problem.description}, {problem.complicacy}";
            }
            else
            {
                return base.Handle(problem);
            }
        }

    }

    public class Major : AbstractHandler
    {
        public override object Handle(Problem problem)
        {
            if (problem.complicacy >= 11 && problem.complicacy <= 100)
            {
                return $"Major zajął się {problem.description}, {problem.complicacy}";
            }
            else
            {
                return base.Handle(problem);
            }
        }

    }

    public class Problem
    {
        public int complicacy { get; set; }
        public string description { get; set; }
    }
}

