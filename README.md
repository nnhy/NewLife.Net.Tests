# 网络库测试用例  
X组件网络库一共经历了4代：  
1，2005年，APM模型，反向代理、点卡服务端  
2，2010年，SAEA模型，P2SP网络，单机7万连接  
3，2014年，物联网云平台，ApiServer单机84.5万连接  
4，2018年，高速RPC，ApiServer单机16万tps  

## 说明文档  
[NewLife.Net——开始网络编程](https://www.cnblogs.com/nnhy/p/newlife_net_echo.html)  
[NewLife.Net——构建可靠的网络服务](https://www.cnblogs.com/nnhy/p/newlife_net_agent.html)  
[NewLife.Net——网络压测单机2266万tps](https://www.cnblogs.com/nnhy/p/newlife_net_benchmark.html)  
[NewLife.Net——管道处理器解决粘包](https://www.cnblogs.com/nnhy/p/newlife_net_handler.html)  
[NetCore版RPC框架NewLife.ApiServer](https://www.cnblogs.com/nnhy/p/newlife_apiserver.html)  

## 压力测试  
NewLife.Net压力测试，峰值4.2Gbps，50万pps，消息大小24字节，消息处理速度2266万tps！  
共集合20台高配ECS参与测试，主服务器带宽6Gbps、100万pps，16核心64G内存。另外19台共模拟400个用户连接，13*16+6*32=400，每用户发送2000万个消息，服务端收到后原样返回。  
![Agent2][Doc/Benchmark/Agent2.png]
![全貌][Doc/RpcTest/全貌.png]

#### 新生命开发团队  
网站：http://www.NewLifeX.com  
博客：https://nnhy.cnblogs.com  
QQ群：1600800  

## 项目源码位置
`注意：X组件具有15年漫长历史，源码库保留有2010年以来所有修改记录，并一直保持更新，请确保获取得到最新版本源代码`  
国内 http://git.NewLifeX.com/NewLife/X  
国外 https://github.com/NewLifeX/X  
