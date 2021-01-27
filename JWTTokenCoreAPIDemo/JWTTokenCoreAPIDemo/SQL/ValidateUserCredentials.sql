select 
	UserId, 
	UserName,
	Password,
	Roles
from Users
where
	UserName =@UserName and
	Password = @Password
for JSON auto