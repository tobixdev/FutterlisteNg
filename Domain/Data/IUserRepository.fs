namespace FutterlisteNg.Domain.Data

open FutterlisteNg.Domain.Model
open MongoDB.Driver

type IUserRepository =
    abstract All: unit -> seq<User>
    abstract Add: User -> unit

type UserRepository() =
    let ConnectionString = ""
    let DatabaseName = "Futterliste"
    let CollectionName = "User"
    let client = MongoClient(ConnectionString)
    let db = client.GetDatabase(DatabaseName)
    let users = db.GetCollection<User>(CollectionName)

    interface IUserRepository with
        member this.All() = users.Find(Builders.Filter.Empty).ToEnumerable()
        member this.Add(user) = users.InsertOne(user)
