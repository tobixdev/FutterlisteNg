namespace FutterlisteNg.Domain.Services
    
open FutterlisteNg.Domain.Model
open FutterlisteNg.Domain.Data
    
type IUserService =
    abstract member All : seq<User>
    
type UserService(repository : IUserRepository) =
    interface IUserService with
        member this.All = repository.All