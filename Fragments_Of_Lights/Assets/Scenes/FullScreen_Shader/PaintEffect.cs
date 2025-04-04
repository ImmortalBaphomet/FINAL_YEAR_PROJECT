using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class PaintEffect : ScriptableRendererFeature
{
    class PaintingEffectPass : ScriptableRenderPass
    {
        private Material material;
        private RenderTargetHandle tempTexture;

        public PaintingEffectPass(Material mat)
        {
            material = mat;
            tempTexture.Init("_TempPaintingEffect");
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            if (material == null) return;

            // Correct way to access the camera color target in Unity 2023+
            RenderTargetIdentifier source = renderingData.cameraData.renderer.cameraColorTarget;

            CommandBuffer cmd = CommandBufferPool.Get("PaintingEffect");

            RenderTextureDescriptor opaqueDesc = renderingData.cameraData.cameraTargetDescriptor;
            opaqueDesc.depthBufferBits = 0; // Optional: improve performance if depth is not needed

            // Get temporary texture
            cmd.GetTemporaryRT(tempTexture.id, opaqueDesc, FilterMode.Bilinear);

            // Apply the material effect
            Blit(cmd, source, tempTexture.Identifier(), material);
            Blit(cmd, tempTexture.Identifier(), source);

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

        public override void FrameCleanup(CommandBuffer cmd)
        {
            if (cmd != null)
            {
                cmd.ReleaseTemporaryRT(tempTexture.id);
            }
        }
    }

    public Material material;
    private PaintingEffectPass paintingEffectPass;

    public override void Create()
    {
        paintingEffectPass = new PaintingEffectPass(material)
        {
            renderPassEvent = RenderPassEvent.AfterRenderingTransparents
        };
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(paintingEffectPass);
    }

}
