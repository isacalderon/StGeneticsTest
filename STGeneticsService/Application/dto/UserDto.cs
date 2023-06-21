public class UserDto{
    public string userName { get; set; }

    public string password { get; set; }

    public UserDto( string userName, string password){
        this.userName = userName;
        this.password = password;
    }
}