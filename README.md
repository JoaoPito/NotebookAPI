# NotebookAPI
NotebookAPI is a RESTful web API made as a tool to help me organize and locate my study notes.
I made this tool mainly to centralize all my notes, since they were all scattered around as my study methods evolved over time. Using this API in a server has the advantage to locate any note with a given subject, doesn't matter if is stored in a .txt file or in a physical paper notebook.

> ⚠️ **WARNING!**<br>
> This API is **not, by any means, production-ready**. This means that fatal errors can (and will) happen.

## Features
It can store information about my notes, such as **tags**, **where it is located**, **last modified date** in an integrated SQLite database.
It also has a **search tool**, and can **filter** notes by tag.

This API was made using ASP.NET and EF Core in C#. It can run on Windows, Linux or macOS, but has only been tested and developed in Linux.

## Usage
### Managing notes:
All CRUD requests to the notes are made to the URL **localhost:[PORT]/Note**, all the request and responses are in JSON format.
- **To add a new note:** Make a POST request with the fields **"_Title_", and "_Directory_"**.
- **To retrieve/delete/update any specific note:** Make a GET/DELETE/PUT/PATCH request to the URL **followed by the note ID**.
<br>Example: localhost:7056/Note/123.
- **To retrieve all notes:** Make a GET request to localhost:[PORT]/Note

### Managing tags:
All CRUD requests to the tags are made to the URL **localhost:[PORT]/Tag** with the field **_Name_** for the tag's name. The process is the same as the notes.

### Relating notes to tags:
With a note and a tag created, make a POST to the URL **localhost:[PORT]/NoteTag**, with the fields **_NoteId_** and **_TagId_** correctly filled.
To remove or update a relationship, just make a DELETE or PUT, or a GET to retrieve it.

Be sure that the binaries have the necessary system permissions to host a HTTP server

## To do
- [x] ~~Implement CRUD for notes and tags~~
- [x] ~~Paging, search and filtering for notes~~
- [ ] Error handling
