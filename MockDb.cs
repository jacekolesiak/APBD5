namespace APBD5;

public interface IMockDb
    {
        ICollection<Animal> GetAllAnimals();
        ICollection<Visit> GetAllVisits();
        void AddAnimal(Animal animal);
        void RemoveAnimal(Animal animal);
        void AddVisit(Visit visit);
    }

    public class MockDb : IMockDb
    {
        private ICollection<Animal> _animals;
        private ICollection<Visit> _visits;
        private int _nextAnimalId = 1;
        private int _nextVisitId = 1;

        public MockDb()
        {
            _animals = new List<Animal>();
            _visits = new List<Visit>();

            // Adding example animals
            Animal reksio = new Animal
            {
                Id = _nextAnimalId++,
                Name = "Reksio",
                Category = "dog",
                Mass = 35,
                FurColor = "White"
            };

            Animal filemon = new Animal()
            {
                Id = _nextAnimalId++,
                Name = "Filemon",
                Category = "cat",
                Mass = 25,
                FurColor = "Black"
            };

            _animals.Add(reksio);
            _animals.Add(filemon);

            // Adding example visits
            _visits.Add(new Visit()
            {
                Id = _nextVisitId++,
                Date = DateTime.Parse("2024-04-15"),
                Animal = reksio,
                Description = "none",
                Price = 100
            });

            _visits.Add(new Visit()
            {
                Id = _nextVisitId++,
                Date = DateTime.Parse("2024-04-16"),
                Animal = filemon,
                Description = "none",
                Price = 200
            });
        }

        public ICollection<Animal> GetAllAnimals()
        {
            return _animals;
        }

        public ICollection<Visit> GetAllVisits()
        {
            return _visits;
        }

        public void AddAnimal(Animal animal)
        {
            animal.Id = _nextAnimalId++;
            _animals.Add(animal);
        }

        public void RemoveAnimal(Animal animal)
        {
            _animals.Remove(animal);
        }

        public void AddVisit(Visit visit)
        {
            visit.Id = _nextVisitId++;
            _visits.Add(visit);
        }
    }