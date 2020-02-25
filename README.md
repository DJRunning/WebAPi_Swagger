# WebAPi_Swagger
WebAPI中使用Swagger

1、安装包 Swashbuckle

    会自动生成 SwaggerConfig.cs文件

2、右键项目属性—>生成—>勾选XML文档文件

　　eg  bin\SwaggerTest.xml    【若对api写了注释，并在swagger中 开启了，则会自动生成一些说明节点】

3、在SwaggerConfig类中，EnableSwagger的时候添加下面XML解析（默认是有的，只是注释掉了）

     c.IncludeXmlComments(string.Format("{0}/bin/SwaggerTest.XML", System.AppDomain.CurrentDomain.BaseDirectory));//启用注释文档

     c.SingleApiVersion("v1", "SwaggerTest接口在线文档");//声明Swagger版本及标题
     c.OperationFilter<HttpAuthHeaderFilter>();//swagger 增加 Auth-Token 选项(请求头)
     c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());//解决action 方法名称相同引发的异常
     
     //自定义的swagger显示控制器的描述（汉化）
     c.CustomProvider((defaultProvider) => new SwaggerCacheProvider(defaultProvider, string.Format("{0}/bin/SwaggerTest.XML", System.AppDomain.CurrentDomain.BaseDirectory)));
     c.InjectJavaScript(System.Reflection.Assembly.GetExecutingAssembly(), "SwaggerTest.Js.swagger.js");
 
 4、项目中涉及到了认证与授权在ApiAuthorizationFilterAttribute类中
