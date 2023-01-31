namespace stc_discernment_server.Models;

public class Configuration {

    public string KeyValue { get; set; } = string.Empty;
    public string DataValue { get; set; } = string.Empty;
    public string Note { get; set; } = string.Empty;
    public bool System { get; set; } = false;
    public bool Active { get; set; } = true;
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime? Updated { get; set; } = null;

}
