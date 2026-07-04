window.tourism = {
    map: null,
    markers: [],
    
    initMap: function (elementId, locations) {
        if (this.map) {
            this.map.remove();
        }
        
        var element = document.getElementById(elementId);
        if (!element) return;

        // Default center (Africa) if no locations
        var center = [-15.0, 25.0];
        var zoom = 4;

        if (locations && locations.length > 0) {
            center = [locations[0].lat, locations[0].lng];
            zoom = locations.length === 1 ? 13 : 6;
        }

        this.map = L.map(elementId).setView(center, zoom);

        L.tileLayer('https://{s}.basemaps.cartocdn.com/rastertiles/voyager/{z}/{x}/{y}{r}.png', {
            attribution: '&copy; OpenStreetMap contributors &copy; CARTO',
            subdomains: 'abcd',
            maxZoom: 20
        }).addTo(this.map);

        var bounds = L.latLngBounds();

        if (locations) {
            locations.forEach(loc => {
                var marker = L.marker([loc.lat, loc.lng]).addTo(this.map);
                
                var popupContent = `
                    <div style="text-align:center; padding: 4px;">
                        <div style="font-weight:bold; margin-bottom:4px; font-size:14px; color:#003B95;">${loc.title}</div>
                        <div style="font-weight:bold; color:#008A0E;">R${loc.price}</div>
                    </div>
                `;
                marker.bindPopup(popupContent);
                bounds.extend([loc.lat, loc.lng]);
            });

            if (locations.length > 1) {
                this.map.fitBounds(bounds, { padding: [50, 50] });
            }
        }
    }
};