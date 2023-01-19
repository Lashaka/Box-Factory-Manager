using System;

namespace Class_Library_Logic
{
    public class Exceptions
    {
        private string _Name;
        private string _Description;
        private string _Explanation;
        private string _Time;


  

        public Exceptions(string name = "", string description = "", string explanation = "")
        {
            _Name = name;
            _Description = description;
            _Explanation = explanation;

        }

        public class IlegalBookAddingException : Exceptions
        {

            public IlegalBookAddingException(string explanation = "") : base()
            {
                DateTime Now = DateTime.Now;

                _Name = "IlegalBookAdding";
                _Description = "Tried to add a book with missing some parameters.";
                _Explanation = explanation;
                _Time = Now.ToString();

            }
        }
    }
}
