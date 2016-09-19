using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Core.Domain;
using Framework.Models;
using Framework.Persistence;

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
