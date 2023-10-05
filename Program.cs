using System;
using System.ComponentModel.DataAnnotations;

namespace Generics_in_dotnet
{
    // Simple Generic Class Defination.
    public class SimpleGenericClass<T>
    {
        public T Field;

        // Generic Method Defination

        public T MyGenericMethod<T>(T arg)
        {
            T temp = arg;
            return temp;
        }
    }

    public class GenericList<T>
    {
        public void Add(T imput) { }
    }


    // Generic Link-List Class

    public class GenericLinkList<T>  // Type parameter in T in angle brackets
    {
        // This nested class is also generic on T
        private class Node
        {
            // T as a private data member datatype
            private T data;

            // T as return type of property

            public T Data
            {
                get { return data; }
                set { data = value; }
            }

            // Similarly 

            private Node? next;
            public Node? Next
            {
                get { return next; }
                set { next = value; }
            }

            //Non-Generic Constructor with type parameter T

            public Node(T t)
            {
                next = null;
                data = t;

            }

        }

        private Node? head;

        // Constuctor of Link-List class
        public GenericLinkList()
        {
            head = null;
        }

        // T as a generic method parameter

        public void AddHead(T t)
        {
            Node n = new Node(t);
            n.Next = head;
            head = n;
        }


        //An IEnumerator<T> is an interface that provides methods for iterating over a collection of elements of type T.   

        public IEnumerator<T> GetEnumerator()
        {
            Node? current = head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }  
    }
    // Example

    public class EmailEventHandler
    { }
    public class SMSRequestHandler
    { }
    public class JSONRequestHandler
    { }
    public class Email
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Content { get; set; }

    }
    public class SMS
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Content { get; set; }
    }
    public class JSON
    {
        public string Source { get; set; }
        public string Destination { get; set; }
        public string Content { get; set; }
    }

    public interface RequestHandler<T>  // An interface to handle events or abstract class and functions
    {
        public bool SendResponse(T obj);
    }
    public class BaseRequestHandler<T> : RequestHandler<T>
    {
        public bool SendResponse(T obj)
        {
            Console.WriteLine(obj.GetType().Name);
            return true;
        }
    }

    class Program
    {
        private class ExampleClass { }
        static void Main(string[] args)
        {
            // Creating an object of generic class with specified string datatype.
            SimpleGenericClass<string> g = new SimpleGenericClass<string>();
            g.Field = "A string";
            Console.WriteLine("SimpleGenericClass.Field           = \"{0}\"", g.Field);
            Console.WriteLine("SimpleGenericClass.Field.GetType() = {0}", g.Field.GetType());
            // Creating an object of generic class with specified int datatype.
            SimpleGenericClass<int> g1 = new SimpleGenericClass<int>();
            g1.Field = 10;
            Console.WriteLine("SimpleGenericClass.Field           = {0}", g1.Field);
            Console.WriteLine("SimpleGenericClass.Field.GetType() = {0}", g1.Field.GetType());

            // Accessing the generic method defined in the GenericClass
            int val = g1.MyGenericMethod(45);
            Console.WriteLine("Value is: {0} ", val);

            // GenericList Class Objects
            
            // Delare a list of type int
            GenericList<int> list1 = new GenericList<int>();
            list1.Add(12);
            // Delare a list of type string
            GenericList<string> list2 = new GenericList<string>();
            list2.Add("");
            // Delare a list of type of another class i.e ExampleClass
            GenericList<ExampleClass> exampleList = new GenericList<ExampleClass>();
            exampleList.Add(new ExampleClass());


            // Generic Link List class Implementation


            GenericLinkList<int> li = new GenericLinkList<int>();  // Creating the list object
            // Adding elements to list
            for(int i = 0; i < 10; i++)
            {
                li.AddHead(i);
            }

            // using a foreach loop or other methods that work with enumerable collections.

            foreach(int i in li)
            {
                Console.WriteLine(i + " ");

            }
            Console.WriteLine("\nDone.....");
            Console.WriteLine();

            // Event handling example


            Email mail = new Email();
            var emailRH = new BaseRequestHandler<Email>();
            emailRH.SendResponse(mail);

            SMS sms = new SMS();
            var smsRH = new BaseRequestHandler<SMS>();
            smsRH.SendResponse(sms);

            JSON json = new JSON();
            var jsonRH = new BaseRequestHandler<JSON>();
            jsonRH.SendResponse(json);



        }

    }


}
