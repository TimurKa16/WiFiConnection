# WifiConnection

2021 year

I have always wanted to hack one's wifi.
But there weren't any programs in the google.
And I was inspired to develop this program.

I approached the question with a brute force.
In our township the most people have the same provider and familiar passwords.
A begining of passwords is the same. So had to guess only the second half of password.
All passwords contained only numbers.

First of all, I used SimpleWifi.
But it works only with known to the computer nets.
Then I used commands from a command prompt.

Algorithm:

1) Delete connection
2) Create XML-file (write a password in hex there)
3) Import XML-file
4) Try to connect to the net
5) Repeat it until we connect succesfully.

I had a little troubles.

1) C# receives a status information from a command prompt in the wrong encoding.
I have cathced and wrote it in string variables.

2) When I connect using an incorrect password, it returns success code.
But it is wrong because then returns fail.
That's why we should connect and wait until it returns fail.
If, on the contrary, we receive success after timeout, it is truly success.

This program is slow. 1 password in 1-2 seconds.
I suppose it's because of OS connection speed.