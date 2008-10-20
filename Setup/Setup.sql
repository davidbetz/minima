--+ account setup
 create login [MinimaSvcDbUser] with password=N'dbsvcpassword', default_database=[Minima], default_language=[us_english], check_expiration=off, check_policy=off
 create user [MinimaSvcDbUser] for login [MinimaSvcDbUser] with default_schema=[svc]
--+ service user
 grant select, insert, update, delete on svc.Author to [MinimaSvcDbUser]
 grant select, insert, update, delete on svc.AuthorBlogAssociation to [MinimaSvcDbUser]
 grant select, insert, update, delete on svc.Blog to [MinimaSvcDbUser]
 grant select, insert, update, delete on svc.BlogEntry to [MinimaSvcDbUser]
 grant select, insert, update, delete on svc.BlogEntryAuthor to [MinimaSvcDbUser]
 grant select, insert, update, delete on svc.BlogEntryCommentAllowStatus to [MinimaSvcDbUser]
 grant select, insert, update, delete on svc.BlogEntryType to [MinimaSvcDbUser]
 grant select, insert, update, delete on svc.BlogImage to [MinimaSvcDbUser]
 grant select, insert, update, delete on svc.BlogEntryStatus to [MinimaSvcDbUser]
 grant select, insert, update, delete on svc.BlogEntryUrlMapping to [MinimaSvcDbUser]
 grant select, insert, update, delete on svc.Comment to [MinimaSvcDbUser]
 grant select, insert, update, delete on svc.Label to [MinimaSvcDbUser]
 grant select, insert, update, delete on svc.LabelBlogEntry to [MinimaSvcDbUser]
 grant select, insert, update, delete on svc.UserRight to [MinimaSvcDbUser]
 grant select, insert, update, delete on svc.BlogEntryCommentAllowStatus to [MinimaSvcDbUser]
 grant exec on svc.[GetArchivedEntryList] to [MinimaSvcDbUser]