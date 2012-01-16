String.prototype.formatWith = function(params) {
   var str = this;
   params = params || { };
   for (var name in params) {
      str = str.replace('{' + name + '}', params[name]);
   }
   
   return str;
};