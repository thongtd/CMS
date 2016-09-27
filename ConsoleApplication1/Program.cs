using System;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var unitOfWork = new UnitOfWork(new WorkContext()))
            {
                var studentModel = new StudentModel
                {
                    CreatedDate = DateTime.Now,
                    DateOfBirth = DateTime.Now,
                    FirstName = "Thong",
                    LastName = "Tran"
                };

                Student student = studentModel;

                unitOfWork.Student.Add(student);
                unitOfWork.Complete();
            }
        }
    }
}
