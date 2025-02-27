﻿// System.Runtime.Remoting.Channels.ChannelServices
// System.Runtime.Remoting.Channels.ChannelServices.RegisteredChannels
// System.Runtime.Remoting.Channels.ChannelServices.UnregisterChannel(IChannel[])

/*
   The following example demonstrates the property 'RegisteredChannels'
   of the class 'ChannelServices', its method 'UnregisterChannel',
   and usage of the class 'ChannelServices'. The example demonstrates
   the remoting version of a server. When a client calls the
   'HelloMethod' on the 'HelloServer' class, the server object appends the
   string passed from the client to the string "Hi There" and returns the
   resulting string back to the client.
*/
using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels.Http;
using System.Security.Permissions;

namespace RemotingSamples
{
   public class MyChannelServices_Server
   {
      [PermissionSet(SecurityAction.LinkDemand)]
      public static void Main()
      {
         try
         {
// <Snippet1>
            // Create and register 'HttpChannel'.
            HttpChannel myHttpChannel = new HttpChannel(8085);
            ChannelServices.RegisterChannel(myHttpChannel);
            // Create and register 'TcpChannel'.
            TcpChannel myTcpChannel = new TcpChannel(8080);
            ChannelServices.RegisterChannel(myTcpChannel);
// <Snippet2>
            // Retrieve and print information about the registered channels.
            IChannel[] myIChannelArray = ChannelServices.RegisteredChannels;
            for (int i=0; i<myIChannelArray.Length; i++)
            {
               Console.WriteLine("Name of Channel: {0}", myIChannelArray[i].ChannelName);
               Console.WriteLine("Priority of Channel: {0}",
                  +myIChannelArray[i].ChannelPriority);
            }
// </Snippet2>
            RemotingConfiguration.RegisterWellKnownServiceType(Type.GetType
               ("RemotingSamples.HelloServer,ChannelServices_RegisteredChannels_Share"),
               "SayHello", WellKnownObjectMode.SingleCall);
// <Snippet3>
            System.Console.WriteLine("Hit <enter> to unregister the channels...");
            System.Console.ReadLine();
            // Unregister the 'HttpChannel' and 'TcpChannel' channels.
            ChannelServices.UnregisterChannel(myTcpChannel);
            ChannelServices.UnregisterChannel(myHttpChannel);
            Console.WriteLine("Unregistered the channels.");
// </Snippet3>
            System.Console.WriteLine("Hit <enter> to exit...");
            System.Console.ReadLine();
// </Snippet1>
         }
         catch(Exception e)
         {
            Console.WriteLine("Exception caught!!!");
            Console.WriteLine("The source of exception: "+e.Source);
            Console.WriteLine("The Message of exception: "+e.Message);
         }
      }
   }
}
