/** Excerpt from Themelia Client Services  **/
/**   Copyright (c) 2008 David Betz [MVP] (http://www.netfxharmonics.com/) **/
Namespace.create('Themelia');
//+
Themelia.AspNet = {
    _objects: new Object( ), 

    registerObject: function(clientId, aspNetId, encapsulated) {
        if(!aspNetId) {
            aspNetId = clientId;
        }
        
        if((!!encapsulated) == true) {
            eval('Themelia.AspNet._objects.' + clientId + ' = $(aspNetId)');
        }
        else {
            eval('window.' + clientId + ' = $(aspNetId)');
        }
    }
};