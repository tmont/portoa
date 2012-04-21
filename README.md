# Porota
by [Tommy Montgomery](http://tmont.com)

## What is Portoa?
*Portoa* is a convenience library/framework for creating and managing an ASP.NET MVC
application that uses [Unity](http://unity.codeplex.com/) and [NHibernate](http://nhforge.org/)
(NHibernate is optional, Unity is kind of required).

Basically, it wires everything up (e.g. ControllFactory, the Unity Container, etc.) so that you
don't have to. It also provides some other utilities, like JSON parsing (default implementation
uses [Json.NET](http://james.newtonking.com/projects/json-net.aspx)), logging interfaces (default
implementation is [log4net](http://logging.apache.org/log4net/)), searching using
[Lucene.NET](http://incubator.apache.org/lucene.net/), and lot of easy-to-use ASP.NET MVC support
(custom model binders and such)
