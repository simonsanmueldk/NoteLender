﻿using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using Sep3Blazor.Data.Notifications.NotificationModel;
using Sep3Blazor.Model;

namespace Sep3Blazor.Data.NoteData
{
    public class NoteService : INoteService
    {
        /// <summary>
        /// Instance variables, uri defines localhost
        /// </summary>
        private readonly String URL = "https://localhost:5004";
        
        
        /// <summary>
        /// Method shows notes using GetNoteListAsync from the businessServer
        /// </summary>
        /// <param name="s"></param>
        /// <returns>Notification</returns>
        public async Task<IList<Note>> GetNoteList(string s)
        {
            using var channel = GrpcChannel.ForAddress(URL);
            var client = new BusinessServer.BusinessServerClient(channel);
            try
            {
                var reply = await client.GetNoteListAsync(
                    new Request {Name = s});
                return JsonSerializer.Deserialize<List<Note>>(reply.Message);
            }
            catch (RpcException e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// Method adds note using PostNoteAsync from the businessServer
        /// </summary>
        /// <param name="note"></param>
        /// <returns>Notification</returns>
        public async Task<Notification> AddNote(Note note)
        {
            using var channel = GrpcChannel.ForAddress(URL);
            var client = new BusinessServer.BusinessServerClient(channel);
            try
            {
                // Send note to GRPC service.
                var reply = await client.PostNoteAsync(
                    new NoteRequest
                    {
                        NoteId = note.id,
                        GroupId = note.groupId,
                        Week = note.week,
                        Year = note.year,
                        Name = note.name,
                        Status = note.status,
                        Text = note.text
                    });
                return new Notification("Success", "Note " + note.name + " was successfully created",
                    NotificationType.Success);
            }
            catch (RpcException e)
            {
                Console.WriteLine(e.StackTrace);
                return new Notification("Error",
                    "Note " + note.name + " was not successfully added. ", NotificationType.Error);
            }
        }
        
        /// <summary>
        /// Method makes the user able to edit note using PutNoteAsync from businessServer
        /// </summary>
        /// <param name="note"></param>
        /// <returns>Notification</returns>
        public async Task<Notification> EditNote(Note note)
        {
            using var channel = GrpcChannel.ForAddress(URL);
            var client = new BusinessServer.BusinessServerClient(channel);
            try
            {
                var reply = await client.PutNoteAsync(
                    new NoteRequest
                    {
                        NoteId = note.id,
                        GroupId = note.groupId,
                        Week = note.week,
                        Year = note.year,
                        Name = note.name,
                        Status = note.status,
                        Text = note.text
                    });
                return new Notification("Success", "Note was successfully edited",
                    NotificationType.Success);
            }
            catch (RpcException e)
            {
                Console.WriteLine(e.StackTrace);
                return new Notification("Error", "Note failed to be edited", NotificationType.Error);
            }
        }

        /// <summary>
        /// Method removes note using DeleteNoteAsync from the businessServer
        /// </summary>
        /// <param name="noteId"></param>
        /// <returns>Notification</returns>
        public async Task<Notification> DeleteNote(int noteId)
        {
            using var channel = GrpcChannel.ForAddress(URL);
            var client = new BusinessServer.BusinessServerClient(channel);
            try
            {
                var reply = await client.DeleteNoteAsync(
                    new Request()
                    {
                        Name = noteId.ToString()
                    }
                );
                return new Notification("Success", "Note was successfully deleted",
                    NotificationType.Success);
            }
            catch (RpcException e)
            {
                Console.WriteLine(e.StackTrace);
                return new Notification("Error", "Note failed to be removed from group", NotificationType.Error);
            }
        }

     
    }
}