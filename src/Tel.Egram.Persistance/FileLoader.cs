﻿using System;
using System.Collections.Concurrent;
using System.IO;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using TdLib;
using Tel.Egram.Utils.TdLib;

namespace Tel.Egram.Persistance
{
    public class FileLoader : IFileLoader
    {
        private readonly IAgent _agent;

        public FileLoader(IAgent agent)
        {
            _agent = agent;
        }

        public IObservable<TdApi.File> LoadFile(TdApi.File file, LoadPriority priority)
        {
            if (IsDownloadingNeeded(file))
            {
                var updates = _agent.Updates
                    .OfType<TdApi.Update.UpdateFile>()
                    .Select(u => u.File)
                    .Where(f => f.Id == file.Id)
                    .TakeWhile(f => IsDownloadingNeeded(f));

                var download = Observable.Defer(() => _agent.Execute(new TdApi.DownloadFile
                {
                    FileId = file.Id,
                    Priority = (int) priority
                }));

                var final = Observable.Defer(() => _agent.Execute(new TdApi.GetFile
                {
                    FileId = file.Id
                }));

                return download.Concat(updates).Concat(final);
            }

            return Observable.Return(file);
        }

        private bool IsDownloadingNeeded(TdApi.File file)
        {
            return file.Local == null
                || !file.Local.IsDownloadingCompleted
                || file.Local.Path == null
                || !File.Exists(file.Local.Path);
        }
    }
}