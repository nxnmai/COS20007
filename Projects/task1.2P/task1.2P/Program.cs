namespace task1._2P
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Message myMessage = new Message("Hello, World! Greetings from Message Object. My Student ID is 105549980.");
            myMessage.Print();

            List<Message> messages = new List<Message>();
            messages.Add(new Message("Hi Cantarella, how are you?"));
            messages.Add(new Message("Hi Brant, how are you?"));
            messages.Add(new Message("Hi Verina, how are you?"));
            messages.Add(new Message("Welcome Admin."));
            messages.Add(new Message("Welcome, nice to meet you!"));

            while (true)
            {
                Console.Write("Enter your name: ");
                string name = Console.ReadLine();

                if (name.ToLower() == "exit")
                {
                    break;
                }

                if (name.ToLower() == "cantarella")
                {
                    messages[0].Print();
                }
                else if (name.ToLower() == "brant")
                {
                    messages[1].Print();
                }
                else if (name.ToLower() == "verina")
                {
                    messages[2].Print();
                }
                else if (name.ToLower() == "mai")
                {
                    messages[3].Print();
                }
                else
                {
                    messages[4].Print();
                }
            }
        }
    }
}
