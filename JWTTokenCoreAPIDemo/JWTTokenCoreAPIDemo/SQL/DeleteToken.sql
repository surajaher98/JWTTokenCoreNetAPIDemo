  Delete 
  FROM TokenInfo
  WHERE 
		UserId = @UserId and
		RefreshToken= @RefreshToken and
		AccessToken = @AccessToken