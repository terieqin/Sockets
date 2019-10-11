# Sockets
Learn how to establish a tcp link using every program language

### 1.
TCP链接一开始必须有一端进行端口监听，另一端进行主动连接。主动的一方叫客户端，监听端口并等待连接的叫服务器端。建立链接之后就没有差别了，双方搜可以接收消息。

客户端使用connect()的方式进行主动连接，服务器端需要用bind()绑定端口，然后用listen()进行监听。

服务器端一个进程要准备TCP链接的时候，一般是占用一个端口进行TCP监听，即listen，然后每当监听到一个连接请求，就会用accept返回一个新的socket。最后实际上和客户端进行业务交互的是新的socket（也意味着新的端口）。这导致在客户端建立链接后，用netstat打印出来的远程的端口并不是用connect发起请求的端口。