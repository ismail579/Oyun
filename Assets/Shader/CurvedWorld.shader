Shader "Custom/CurvedWorld" {
    Properties {
        _MainTex ("Kaplama (Texture)", 2D) = "transparent" {}
        _Color ("Renk", Color) = (1,1,1,1)
        
        // Bükülme şiddetini ayarlayacağımız değişkenler
        _CurveX ("Sola / Sağa Bükülme", Float) = -0.001
        _CurveY ("Aşağı / Yukarı Bükülme", Float) = -0.001
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // vertex:vert ile objenin köşelerini (vertex) hareket ettireceğimizi söylüyoruz
        #pragma surface surf Standard vertex:vert addshadow
        #pragma target 3.0

        sampler2D _MainTex;
        fixed4 _Color;
        float _CurveX;
        float _CurveY;

        struct Input {
            float2 uv_MainTex;
        };

        void vert (inout appdata_full v) {
            // Objenin dünya (world) üzerindeki gerçek pozisyonunu al
            float4 worldPos = mul(unity_ObjectToWorld, v.vertex);

            // Objenin kameraya olan Z eksenindeki uzaklığını hesapla
            float dist = worldPos.z - _WorldSpaceCameraPos.z;
            
            // Sadece kameranın önündeki (ilerideki) objeleri bük (Arkadakileri bozmamak için)
            if (dist > 0) {
                // Uzaklığın karesini alarak pürüzsüz bir parabol (kavis) oluşturuyoruz
                worldPos.y += dist * dist * _CurveY; // Aşağı/Yukarı kavis
                worldPos.x += dist * dist * _CurveX; // Sağa/Sola kavis
            }

            // Bükülmüş pozisyonu tekrar Unity'nin anlayacağı lokal koordinata çevir
            v.vertex = mul(unity_WorldToObject, worldPos);
        }

        void surf (Input IN, inout SurfaceOutputStandard o) {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}