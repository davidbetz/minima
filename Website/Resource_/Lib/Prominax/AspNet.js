/** Excerpt from Prominax ASP.NET Framework  **/
/**   Copyright (c) 2008 David Betz (http://www.netfxharmonics.com/) **/
Namespace.create('Prominax');
//+
Prominax.AspNet = {
    _objects: new Object( ), 

    registerObject: function(clientId, aspNetId, encapsulated) {
        if(!aspNetId) {
            aspNetId = clientId;
        }
        
        if((!!encapsulated) == true) {
            eval('Prominax.AspNet._objects.' + clientId + ' = $(aspNetId)');
        }
        else {
            eval('window.' + clientId + ' = $(aspNetId)');
        }
    }
};