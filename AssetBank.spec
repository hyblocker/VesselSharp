[VSL-BNK]
unsigned int16 version;          // integer version
unsigned int64 fileBlob;         // size of the file table in bytes
[FileTable] table;               // the file table itself, may be encrypted
Array<[File]> files;             // the file data itself, unencrypted to save performance



[FileTable]
[header]
unsigned int64 size;             // Number of unique files in the file table
unsigned int32 fileSize;         // How big each file blob is in the file table
[/header]
OrderedList<[FileBlob]> files;   // An array of [FileBlobs], each laid out one after the other





[File]
[header]
unsigned int64 uuid;             // FileName Hash
unsigned int64 length;           // How large the file is, excluding metadata and the header
unsigned int64 initPtr;          // First pointer of the file data relative to the head of the file blob
unsigned int64 blobSize;         // How large the BSON Meta Blob is in bytes;
[/header]
byte[] BSONBlob;                 // The actual BSON Blob
byte[] FileBytes;                // The file data
[footer]
unsigned int64 padding;          // How many bytes are to be ignored when going to the next file
[/footer]

// Notes:
// 
// Padding is after the file data to make it even harder to understand the file table without decryption
// Padding bytes are to have random data generated to confuse reverse engineers, which is to be ignored by the actual engine
// The file table is loaded into memory on startup and decrypted, if we fail, we show a special image file that says that the assets are corrupt, and suggesting the user to verify their integrity. Developers can override this message if they wish to do so by disabling the default message and checking if AssetManager.AssetsTampered or AssetManager.AssetsCorrupt. To disallow modding the tampered and corrupt ones should be used, corrupt check should be done too

