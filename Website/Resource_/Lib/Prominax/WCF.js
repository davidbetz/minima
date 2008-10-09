/** Excerpt from Themelia Client Services  **/
/**   Copyright (c) 2008 David Betz [MVP] (http://www.netfxharmonics.com/) **/
Namespace.create('Themelia');
//+
Themelia.WCF = {
    //- post -//
    post: function(params) {
        // params: {
        //  endpoint,
        //  operation,
        // }
        // + the rest of the prototype [Ajax Options] http://prototypejs.org/api/ajax/options
        Themelia.WCF._validateGeneral(params);
        if(!params.message) { throw 'message is required' }
        //+
        Themelia.WCF._submit(Object.extend(params, {
            method: 'post',
            postBody: Object.toJSON(params.message)
        }));
    },
    
    //- get -//
    'get': function(params) {
        Themelia.WCF._validateGeneral(params);
        //+
        Themelia.WCF._submit(Object.extend(params, {
            method: 'get'
        }));
    },
    
    //- delete -//
    'delete': function(params) {
        Themelia.WCF._validateGeneral(params);
        //+
        Themelia.WCF._submit(Object.extend(params, {
            method: 'delete'
        }));
    },
    
    //- _validateGeneral -//
    _validateGeneral: function(params) {
        if(params) {
            if(typeof params.endpoint == 'undefined') { throw 'endpoint is required' }
            if(typeof params.operation == 'undefined') { throw 'operation is required' }
        }
        else {
            throw 'params is required';
        }
    },
    
    //- _submit -//
    _submit: function(params) {
        var _onSuccess = params.onSuccess;
        new Ajax.Request(params.endpoint + '/' + params.operation, Object.extend(params, {
            parseJSON: params.parseJSON || true,
            method: params.method,
            contentType: 'application/json',
            onSuccess: function(r) {
                if(typeof _onSuccess != undefined) {
                    if(!!params.parseJSON == true) {
                        _onSuccess(r.responseJSON);
                    }
                    else {
                        _onSuccess(r);
                    }
                }
            },
            onException: params.onException || Themelia.WCF.globalFaultHandler || Prototype.emptyFunction,
            onFailure: params.onFailure || Themelia.WCF.globalFaultHandler || Prototype.emptyFunction
        }));
    }
};