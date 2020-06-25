using System;
using System.Linq;
using BasicOperations.Entities;
using BasicOperations.Models;
using ConsoleTables;

namespace BasicOperations
{
    class Program
    {
        static void Main(string[] args)
        {
            //Display content of actors table
            var actors = MoviesContext.Instance.Actor;
            ConsoleTable.From(actors).Write();

            //Disaplay content using model class
            var actorModels = MoviesContext.Instance.Actor.Select(a =>
                 new ActorModel { ActorId = a.ActorId, FirstName = a.FirstName, LastName = a.LastName });
            ConsoleTable.From(actorModels).Write();

            //Display single item
            var actorTom = MoviesContext.Instance.Actor.SingleOrDefault(a => a.FirstName == "Tom");
            Console.WriteLine($"Id: {actorTom.ActorId}, FirstName: {actorTom.FirstName}, LastName: {actorTom.LastName}");
            
            //Add an actor
            var newActor = new Actor { FirstName = "Joaquin", LastName = "Phoenix" };
            MoviesContext.Instance.Actor.Add(newActor);
            MoviesContext.Instance.SaveChanges();

            //Update an actor
            var updateActor = MoviesContext.Instance.Actor.SingleOrDefault(actorModels => actorModels.ActorId == 22);
            updateActor.FirstName = "Leonardo";
            updateActor.LastName = "DiCaprio";
            MoviesContext.Instance.SaveChanges();

            //Delete an item
            var removeActor = MoviesContext.Instance.Actor.SingleOrDefault(a => a.ActorId == 23);
            if (removeActor != null)
            {
                MoviesContext.Instance.Actor.Remove(removeActor);
                MoviesContext.Instance.SaveChanges();
            }

        }
    }
}
