SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[dateEdit]
ON [dbo].[Event]
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Always set DateUpdated to the current timestamp
    UPDATE Event
    SET 
        DateEdited = GETDATE(),
        UserID = (SELECT UserID FROM deleted WHERE Event.EventID = deleted.EventID),
        DateCreated = (SELECT DateCreated FROM deleted WHERE Event.EventID = deleted.EventID)
    FROM inserted
    WHERE Event.EventID = inserted.EventID;
END;
GO
ALTER TABLE [dbo].[Event] ENABLE TRIGGER [dateEdit]
GO
