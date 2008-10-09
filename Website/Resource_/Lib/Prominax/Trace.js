/** Excerpt from Themelia Client Services  **/
/**   Copyright (c) 2008 David Betz [MVP] (http://www.netfxharmonics.com/) **/
Namespace.create('Themelia');
//+
Themelia.Trace = {
  _enabled: false,
  // by default Firefox uses the Firefox Console and Safari and Opera uses their JavaScript Console
  alwaysUseFirebug: false,
  _counter: 0,
  _isIE: !!(window.attachEvent && !window.opera),
  _isMozilla: navigator.userAgent.indexOf('Gecko')>-1 && navigator.userAgent.indexOf('KHTML')==-1,
  _isWebKit: navigator.userAgent.indexOf('KHTML')>-1,
  _isOpera: !!window.opera
};

Themelia.Trace.isSupported = function( ) {
    return typeof(Themelia.Trace._isMozilla || Themelia.Trace._isWebKit || Themelia.Trace._isOpera || window.debugService || window.console) != 'undefined'
}

Themelia.Trace.write = function(text) {
  if(Themelia.Trace._enabled == true && Themelia.Trace.isSupported( ) == true) {
    if(Themelia.Trace.alwaysUseFirebug == true) {
      console.log(text);
    }
    else if(window.debugService) {
      window.debugService.trace(text);
    }
    else if(Themelia.Trace._isMozilla == true) {
      dump(text);
    }
    else if(Themelia.Trace._isWebKit == true) {
      window.console.log(text);
    }
    else if(Themelia.Trace._isOpera == true) {
      opera.postError(text);
    }
  }
}

Themelia.Trace.enable = function( ) {
  Themelia.Trace._enabled = true;
}

Themelia.Trace.addNewLine = function( ) {
  if(Themelia.Trace._enabled == true && Themelia.Trace.isSupported( ) == true) {
    Themelia.Trace.write('\n');
  }
}

Themelia.Trace.writeLine = function(text) {
  text = text || '';
  if(Themelia.Trace._enabled == true && Themelia.Trace.isSupported( ) == true) {
    Themelia.Trace._counter++;
    if(!window.debugService) {
        text = text + '\n';
    }
    Themelia.Trace.write(Themelia.Trace._counter + ':' + text + '\n');
  }
}

Themelia.Trace.writeLabeledLine = function(text, value) {
  text = text || '';
  if(Themelia.Trace._enabled == true && Themelia.Trace.isSupported( ) == true) {
    Themelia.Trace._counter++;
    Themelia.Trace.write(Themelia.Trace._counter + ':' + text + ' (' + value + ')\n');
  }
}

Themelia.Trace.Buffer = function(useAlert) {
  this._stringBuilder = [];
  this._useAlert = !!useAlert;
  this._depth = 0;
  var that = this;
  
  this.getIndent = function ( ) {
    var indent = '';
    for(var i=0;i<this._depth;i++) {
      indent += '  ';
    }
    return indent;
  }
  
  return {
    beginSegment: function(title) {
      if(title && title.length > 0) {
        that._stringBuilder.push(that.getIndent( ) + '++' + title);
        that._depth++;
      }
    },
    
    endSegment: function(title) {
      if(title && title.length > 0) {
        that._depth--;
        that._stringBuilder.push(that.getIndent( ) + '--' + title + '\n');
      }
    },

    write: function(text) {
      text = text || '';
      if(typeof text == 'number' || typeof text == 'string') {
        that._stringBuilder.push(that.getIndent( ) + text);
      }
      else if(typeof m == 'object' && 'join' in m) {
        this._writeArray(text);
      }
      else {
        this._writeObject(text);
      }
    },
    
    _writeArray: function(array) {
      if(array && array.length > 0) {
        var first = true;
        var c = array.length;
        for(var n=0; n<c; n++) {
          if(array[n]) {
            this.write('[');
            that._depth++;
            this.write(array[n]);
            that._depth--;
            first = false;
            if(n+1<c) {
              this.write('],');
            }
            else {
              this.write(']');
            }
          }
        }
      }
    },
    
    _writePair: function(pair) {
      this.write(pair + ':' + pair[0]);
    },
    
    _writeObject: function(obj) {
      for(var o in obj) {
        if(obj[o] && typeof obj[o] != 'function') {
          var m = obj[o];
          if(typeof m == 'object' && 'join' in m) {
            this.beginSegment(o+'[]');
            this._writeArray(m);
            this.endSegment(o+'[]');
          }
          else if(typeof obj[o] == 'number' || typeof obj[o] == 'string') {
            this.write(o + ':' + m);
          }
          else {
            this.beginSegment(o);
            this._writeObject(m);
            this.endSegment(o);
          }
        }
      }
    },

    writeNewLine: function( ) {
      this.write('\n');
    },

    flush: function(text) {
      text = text || '';
      var data = that._stringBuilder.join('\n');
      if(that._useAlert == true) {
        alert(data);
      }
      else {
        Themelia.Trace.write(data);
      }
      that._stringBuilder = [];
    }
  }
};

Themelia.Trace.alwaysUseFirebug = false;
Themelia.Trace.enable( );
Themelia.Trace.addNewLine( );