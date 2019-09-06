namespace FutterlisteNg.Domain.Data

type User(name:string, shortName:string) =
    member this.name = name
    member this.shortName = shortName
    
type IUserService =
    abstract member All : seq<User>
    
type UserService() =
    interface IUserService with
        member this.All = Seq.empty