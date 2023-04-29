using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class TextureImporterDefaults : AssetPostprocessor
    {
        // 画像をインポートするときの規定値を、今回のプロジェクト用（ドット絵）に合わせる拡張
        void OnPreprocessTexture()
        {
            var importer = (TextureImporter)assetImporter;

            // Pixels Per Unit の設定
            importer.spritePixelsPerUnit = 16;

            // Filter Mode の設定
            importer.filterMode = FilterMode.Point;
        
            // インポート時に MipMaps を無効化
            importer.mipmapEnabled = false;

            // インポート時にテクスチャ圧縮を無効化
            importer.textureCompression = TextureImporterCompression.Uncompressed;
        }
    }
}