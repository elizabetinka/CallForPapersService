using InfrastructureOrm.Model;
using InfrastructureOrm.Model.Activity;
using InfrastructureOrm.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ModelDTO;


namespace InfrastructureOrm;

class Program
{
    static async Task Main(string[] args)
    {
        AdminRepository adminRepository = new AdminRepository();
        UserRepository userRepository = new UserRepository();
        ApplicationRepository applicationRepository = new ApplicationRepository();
        
        await userRepository.AddUser("liza","123");
        await userRepository.AddUser("liza2","1234");
        await adminRepository.AddAdmin("admin","admin");
        Guid? id = await userRepository.GetId("liza", "123");
        if (id != null)
        {
            await userRepository.AddApplication((Guid)id,new ApplicationDTO{Name = "kyky",Activity = "Masterclass"});
        }
        /*
        using (ApplicationContext db = new ApplicationContext())
        {
            //await db.Database.MigrateAsync();
            //await db.Database.EnsureDeletedAsync();
            // создаем два объекта User
            User user1 = new User { password = "Tom2" };
            User user2 = new User {  password = "Alice2" };

            // добавляем их в бд
            db.users.AddRange(user1, user2);
            db.SaveChanges();
        }
        // получение данных

        using (ApplicationContext db = new ApplicationContext())
        {
            await db.Database.MigrateAsync();
            // получаем объекты из бд и выводим на консоль
            var users = db.users.ToList();
            Console.WriteLine("Users list:");
            foreach (User u in users)
            {
                Console.WriteLine($"{u.id}.{u.id} - {u.password}");
            }
        }
        */

    }
}