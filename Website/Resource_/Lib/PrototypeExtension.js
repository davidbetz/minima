Namespace = window.Namespace || { }
Namespace.create = function(ns, separator) {
  ns.split(separator || '.').inject(window, function(parent, child) {
    return parent[child] = parent[child] || { };
  });
};
//+
Enum = Namespace;