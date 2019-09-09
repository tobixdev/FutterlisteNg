namespace FutterlisteNg.Domain.Services
    
open FutterlisteNg.Domain.Model
open FutterlisteNg.Domain.Data
    
type IUserService =
    abstract member All : unit -> seq<User>
    abstract member Add : User -> unit
    
type UserService(repository : IUserRepository) =
    interface IUserService with
        member this.All() = repository.All()
        member this.Add(user) = repository.Add(user)