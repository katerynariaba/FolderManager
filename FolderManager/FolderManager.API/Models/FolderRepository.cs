using System;
using System.Collections.Generic;

namespace FolderManager.Api.Models
{
    public static class FolderRepository
    {
        public static List<FolderViewModel> Get()
        {
            var data = new List<FolderViewModel>()
            {
                new FolderViewModel
                {
                    Id = new Guid().ToString(),
                    Name = "Some nice title",
                    Children = new List<FolderViewModel>
                    {
                        new FolderViewModel
                        {
                            Id = new Guid().ToString(),
                            Name = "First child",
                            Children = new List<FolderViewModel>()
                            {
                                new FolderViewModel
                                {
                                    Id = new Guid().ToString(),
                                    Name = "Second child",
                                    Children = new List<FolderViewModel>()
                                    {
                                        new FolderViewModel
                                        {
                                            Id = new Guid().ToString(),
                                            Name = "Second child"
                                        },
                                        new FolderViewModel
                                        {
                                            Id = new Guid().ToString(),
                                            Name = "Third child",
                                            Children = new List<FolderViewModel>()
                                            {
                                                new FolderViewModel
                                                {
                                                    Id = new Guid().ToString(),
                                                    Name = "Second child"
                                                },
                                                new FolderViewModel
                                                {
                                                    Id = new Guid().ToString(),
                                                    Name = "Third child"
                                                }
                                            }
                                        }
                                    }
                                },
                                new FolderViewModel
                                {
                                    Id = new Guid().ToString(),
                                    Name = "Third child"
                                }
                            }
                        },
                        new FolderViewModel
                        {
                            Id = new Guid().ToString(),
                            Name = "Second child"
                        },
                        new FolderViewModel
                        {
                            Id = new Guid().ToString(),
                            Name = "Third child"
                        }
                    }
                },
                new FolderViewModel
                {
                    Id = new Guid().ToString(),
                    Name = "Some nice title 2",
                    Children = new List<FolderViewModel>
                    {
                        new FolderViewModel
                        {
                            Id = new Guid().ToString(),
                            Name = "First child"
                        },
                        new FolderViewModel
                        {
                            Id = new Guid().ToString(),
                            Name = "Second child"
                        },
                        new FolderViewModel
                        {
                            Id = new Guid().ToString(),
                            Name = "Third child"
                        }
                    }
                },
                new FolderViewModel
                {
                    Id = new Guid().ToString(),
                    Name = "Some nice title 3",
                    Children = new List<FolderViewModel>
                    {
                        new FolderViewModel
                        {
                            Id = new Guid().ToString(),
                            Name = "First child"
                        },
                        new FolderViewModel
                        {
                            Id = new Guid().ToString(),
                            Name = "Second child"
                        },
                        new FolderViewModel
                        {
                            Id = new Guid().ToString(),
                            Name = "Third child",
                            Children = new List<FolderViewModel>()
                            {
                                new FolderViewModel
                                {
                                    Id = new Guid().ToString(),
                                    Name = "First child"
                                },
                                new FolderViewModel
                                {
                                    Id = new Guid().ToString(),
                                    Name = "Second child"
                                },
                                new FolderViewModel
                                {
                                    Id = new Guid().ToString(),
                                    Name = "Third child"
                                }
                            }
                        }
                    }
                }
            };

            return data;
        }
    }
}
